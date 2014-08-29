using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using WPMobileNet.Utils;

namespace WPMobileNet.Service
{
    public class AccelerometerService : BaseService
    {
        #region Constants
        private const float MAGNETIC_FIELD_EARTH_MAX = 30.0f;
        private const float STANDARD_GRAVITY = 9.80665f;
        
        private const int WALKING_DELTA = 60000;
        private const int ACTION_EVAL_TIMER = 30000;
        #endregion

        #region Properties
        private readonly Accelerometer _sensor;

        #region Pedometer
        double[] mScale = new double[2];
        private double mYOffset;
        private double mLimit = 4;
        private double mLastValues;
        private double mLastDirections;
        private double[] mLastExtremes = new double[2];
        private double mLastDiff;
        private int mLastMatch = -1;
        private double[] event_values = new double[3];
        private int stepsInaRow = 0;
        private DateTime lasStepStamp;
        public bool isWalking = false;
        private int deltaTimer = 120000;
        #endregion
        #endregion

        #region Constructor
        public AccelerometerService()
        {
            this._sensor = Accelerometer.GetDefault();
            this._sensor.ReadingChanged += _sensor_ReadingChanged;
            int h = 480;
            mYOffset = h * 0.5f;
            mScale[0] = -(h * 0.5f * (1.0f / (STANDARD_GRAVITY * 2)));
            mScale[1] = -(h * 0.5f * (1.0f / (MAGNETIC_FIELD_EARTH_MAX)));
        }
        #endregion

        #region Events
        public event EventHandler<AccelerometerReadingChangedEventArgs> ReadingChanged;
        public event EventHandler<EnumHelper.PedometerStatus> PedometerStatusChanged;
        public event EventHandler SteepDetected;
        private void _sensor_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            if (this.ReadingChanged != null) ReadingChanged(_sensor, args);
            event_values[0] = args.Reading.AccelerationX;
            event_values[1] = args.Reading.AccelerationY;
            event_values[2] = args.Reading.AccelerationZ;
            CheckSteep();
        }
        #endregion

        #region Methods
        private void CheckSteep()
        {
            double vSum = 0;
            for (int i = 0; i < 3; i++)
            {
                double vaux = mYOffset + event_values[i] * mScale[1];
                vSum += vaux;
            }
            double v = vSum / 3;
            float direction = (v > mLastValues ? 1 : (v < mLastValues ? -1 : 0));
            if (direction == -mLastDirections)
            {
                // Direction changed
                int extType = (direction > 0 ? 0 : 1); // minumum ormaximum?
                mLastExtremes[extType] = mLastValues;
                double diff = Math.Abs(mLastExtremes[extType] - mLastExtremes[1 - extType]);
                System.Diagnostics.Debug.WriteLine("Diff: " + diff);
                if (diff > mLimit)
                {
                    bool isAlmostAsLargeAsPrevious = diff > (mLastDiff * 2 / 3);
                    bool isPreviousLargeEnough = mLastDiff > (diff / 3);
                    bool isNotContra = (mLastMatch != 1 - extType);
                    if (isAlmostAsLargeAsPrevious && isPreviousLargeEnough && isNotContra)
                    {
                        // Step detected, wakeup
                        TimeSpan ts = DateTime.Now - lasStepStamp;
                        if (ts.TotalMilliseconds > 1500) stepsInaRow = 0;
                        
                        stepsInaRow++;
                        if (SteepDetected != null) SteepDetected(null, EventArgs.Empty);
                        // Si el delta es demasiado grande, hay mas de 3
                        // pasos seguidos ajusta deltaT a WALKING_DELTA
                        // (3.5Km/h) y haz un retrigger

                        if (stepsInaRow > 3 && !this.isWalking)
                        {
                            if (deltaTimer > WALKING_DELTA)
                            {
                                deltaTimer = WALKING_DELTA;
                                //mvegaca
                                //sendToApp(last_location);
                                //finmvegaca

                                // sp.edit().putInt("deltaTimer",
                                // deltaTimer)
                                // .commit();

                                // stoast("Walking detected - dT="
                                // + stepsInaRow
                                // + " deltaTimer is now "
                                // + deltaTimer);

                                // Transition to walking = retrigger
                                if (!this.isWalking)
                                {
                                    //mvegaca
                                    //mainTimerHandler
                                    //        .removeCallbacks(mUpdateTimeTask);
                                    //mainTimerHandler.postDelayed(
                                    //        mUpdateTimeTask, deltaTimer);
                                    //finmvegaca
                                }
                            }
                            // stepsInaRow = 0;
                            this.isWalking = true;
                            if (PedometerStatusChanged != null) PedometerStatusChanged(null, EnumHelper.PedometerStatus.Walking);                            
                        }
                        else
                        {
                            // Si ya estamos caminando y está marcado
							// revisemos que el timer no se descontrola
							// por falta de GPS...
                            if (stepsInaRow > 3 && this.isWalking)
                            {
                                if (deltaTimer > WALKING_DELTA * 2)
                                {
                                    deltaTimer = WALKING_DELTA * 2;
                                    //mvegaca
                                    //sendToApp(last_location);
                                    //finmvegaca
                                }
                            }
                        }
                        lasStepStamp = DateTime.Now;
                        mLastMatch = extType;
                    }
                    else
                    {
                        mLastMatch = -1;
                    }
                }
                mLastDiff = diff;
            }
            mLastDirections = direction;
            mLastValues = v;
            TimeSpan ts2 = DateTime.Now - lasStepStamp;            
            if (ts2.TotalMilliseconds > (ACTION_EVAL_TIMER * 2) && isWalking)
            {
                // mas de 5 seg sin pasos detectados =
                // parado... (o en coche quizás)
                this.isWalking = false;
                if (PedometerStatusChanged != null) PedometerStatusChanged(null, EnumHelper.PedometerStatus.Stoped);
            }
        }
        #endregion
    }
}
