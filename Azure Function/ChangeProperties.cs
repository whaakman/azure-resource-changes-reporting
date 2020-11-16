using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChangeHistory
{
    public class ChangeProperties
    {
        private string resourceId;
        private string propertyName;
        private string beforeValue;
        private string afterValue;
        private string changeCategory;
        private string timestamp;

        public ChangeProperties(string resourceId, string propertyName, string beforeValue, string afterValue, string changeCategory, string timestamp) 
        {
            this.resourceId = resourceId;
            this.propertyName = propertyName;
            this.beforeValue = beforeValue;
            this.afterValue = afterValue;
            this.changeCategory = changeCategory;
            this.timestamp = timestamp;
        }

        public string ResourceId 
        {
            get { return resourceId; }
            set { resourceId  = value; }
        }

        public string PropertyName
        {
            get {return propertyName; }
            set { propertyName = value; }
        }

        public string BeforeValue
        {
            get {return beforeValue; }
            set { beforeValue = value; }
        }

        public string AfterValue
        {
            get {return afterValue; }
            set { afterValue = value; }
        }

        public string ChangeCategory
        {
            get {return changeCategory; }
            set { changeCategory = value; }
        }

           public string Timestamp
        {
            get {return timestamp; }
            set { timestamp = value; }
        }

    
    } 
}
