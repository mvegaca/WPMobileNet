using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMobileNet.Service
{
    /// <summary>
    /// This service can help you use temporary and persistent storage, serializes objects to JSON strings and stores and retrieves in and from Application State and Isolated Storage
    /// </summary>
    public class StateService
    {
        /// <summary>
        /// Gets objects from temporary Application State or persistent Application Storage
        /// </summary>
        /// <typeparam name="T">Type of the object in the storage</typeparam>
        /// <param name="key">Key that identifies object</param>
        /// <param name="isIsolatedStorage">True when retrieving from Application Storage, false when retrieving from Application State</param>
        /// <returns>The object retrieved from storage</returns>
        internal T GetState<T>(string key, bool isIsolatedStorage = false)
        {
            if (!isIsolatedStorage)
            {
                if (!PhoneApplicationService.Current.State.ContainsKey(key))
                {
                    return default(T);
                }

                return (T)PhoneApplicationService.Current.State[key];
            }
            else
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                {
                    string serializedObject = IsolatedStorageSettings.ApplicationSettings[key].ToString();
                    return Deserialize<T>(serializedObject);
                }

                return default(T);
            }
        }

        /// <summary>
        /// Stores object in temporary Application State or persistent Application Storage
        /// </summary>
        /// <param name="key">Key to identify the object</param>
        /// <param name="value">Object to store</param>
        /// <param name="isIsolatedStorage">True when storing to Application Storage, false when storing to Application State</param>
        internal void SetState(string key, object value, bool isIsolatedStorage = false)
        {
            if (!isIsolatedStorage)
            {
                if (PhoneApplicationService.Current.State.ContainsKey(key))
                {
                    PhoneApplicationService.Current.State.Remove(key);
                }
                PhoneApplicationService.Current.State.Add(key, value);
            }
            else
            {
                string serializedObject = Serialize(value);
                if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                {
                    IsolatedStorageSettings.ApplicationSettings.Remove(key);
                    IsolatedStorageSettings.ApplicationSettings.Save();
                }
                IsolatedStorageSettings.ApplicationSettings.Add(key, serializedObject);
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        private static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        private static string Serialize(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }


        internal void RegisterAppFrame(object frame)
        {
        }

        internal Task RestoreState()
        {
            return Task.Factory.StartNew(() => { });
        }

        internal Task SaveState()
        {
            return Task.Factory.StartNew(() => { });
        }

        internal System.Collections.Generic.Dictionary<string, object> SessionStateForFrame(object frame)
        {
            return new Dictionary<string, object>();
        }
        public StateService() { }
    }
}
