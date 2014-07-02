using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WPMobileNet.Model;

namespace WPMobileNet.ViewModel
{
    [DataContract]
    public class VMStatus : SimpleViewModelBase
    {
        #region Properties
        private MStatus _model;
        [DataMember]
        public MStatus Model
        {
            get { return _model; }
            set { Set("Model", ref _model, value); }
        }

        internal bool IsEmpty
        {
            get
            {
                if (this.Model == null || this.Model.ModificationTime.Equals(default(DateTime))) return true;
                else return false;
            }
        }
        #endregion

        #region Constructors
        public VMStatus() { this.Model = new MStatus(); }
        #endregion

        #region StaticMembers
        public static VMStatus Empty
        {
            get
            {
                return new VMStatus();
            }
        }
        #endregion

        internal VMStatus Copy()
        {
            VMStatus copy = new VMStatus() { Model = new MStatus(this.Model) };
            return copy;
        }
    }
}
