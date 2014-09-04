using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPMobileNet.Service;

namespace WPMobileNet.ViewModel
{
    public class VMGyrometer : VMBase
    {
        #region Services
        private readonly GyrometerService _gyrometerService;
        #endregion

        #region Properties
        private double _controlCanvasWidth;
        public double ControlCanvasWidth { get { return _controlCanvasWidth; } set { Set("ControlCanvasWidth", ref _controlCanvasWidth, value); } }
        private double _controlCanvasHeight;
        public double ControlCanvasHeight { get { return _controlCanvasHeight; } set { Set("ControlCanvasHeight", ref _controlCanvasHeight, value); } }
        public double ControlCanvasHalfHeight { get { return _controlCanvasHeight / 2; } }
        public double ControlCanvasHalfWidth { get { return _controlCanvasWidth / 2; } }
        private double _controlCircleSize;
        public double ControlCircleSize { get { return _controlCircleSize; } set { Set("ControlCircleSize", ref _controlCircleSize, value); } }
        public double ControlCircleHalfSize { get { return _controlCircleSize / 2; } }
        private System.Double _controlEllipseLeft;
        public System.Double ControlEllipseLeft { get { return _controlEllipseLeft; } set { Set("ControlEllipseLeft", ref _controlEllipseLeft, value); } }
        private System.Double _controlEllipseTop;
        public System.Double ControlEllipseTop { get { return _controlEllipseTop; } set { Set("ControlEllipseTop", ref _controlEllipseTop, value); } }
        #endregion

        #region Constructors
        public VMGyrometer(NavigationService navigationService, GyrometerService gyrometerService)
            : base(navigationService)
        {
            this.ControlCanvasWidth = 400;
            this.ControlCanvasHeight = 400;
            this.ControlCircleSize = 50;
            this.ControlEllipseLeft = (this.ControlCanvasWidth / 2) - this.ControlCircleHalfSize;
            this.ControlEllipseTop = (this.ControlCanvasHeight / 2) - this.ControlCircleHalfSize;

            this._gyrometerService = gyrometerService;
            this._gyrometerService.ReadingChanged += GyrometerServiceReadingChanged;            
        }        
        #endregion

        #region Events
        private void GyrometerServiceReadingChanged(object sender, Windows.Devices.Sensors.GyrometerReadingChangedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.Status.Model.AngularVelocityX = e.Reading.AngularVelocityX;
                this.Status.Model.AngularVelocityY = e.Reading.AngularVelocityY;
                this.Status.Model.AngularVelocityZ = e.Reading.AngularVelocityZ;
                UpdateEllipseValues(e);
            });
        }
        #endregion

        #region Methods
        public void Start() { this._gyrometerService.Start(); }
        public void Stop() { this._gyrometerService.Stop(); }
        private void UpdateEllipseValues(Windows.Devices.Sensors.GyrometerReadingChangedEventArgs e)
        {
            double delta = 1;
            var qtdWidth = e.Reading.AngularVelocityX * delta;
            var qtdHeight = e.Reading.AngularVelocityY * delta;
            var sumWidth = this.ControlEllipseLeft + qtdWidth;
            var sumHeight = this.ControlEllipseTop - qtdHeight;
            if (sumWidth > 0 && sumWidth <= this.ControlCanvasWidth - this.ControlCircleSize)
            {
                this.ControlEllipseLeft = sumWidth;
            }
            if (sumHeight > 0 && sumHeight <= this.ControlCanvasHeight - this.ControlCircleSize)
            {
                this.ControlEllipseTop = sumHeight;
            }
        }
        #endregion
    }
}
