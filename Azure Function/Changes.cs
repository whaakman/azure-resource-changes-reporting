using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChangeHistory
{
    public class BeforeSnapshot    {
        [JsonPropertyName("snapshotId")]
        public string SnapshotId { get; set; } 

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } 
    }

    public class AfterSnapshot    {
        [JsonPropertyName("snapshotId")]
        public string SnapshotId { get; set; } 

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } 
    }

    public class PropertyChanx    {
        [JsonPropertyName("propertyName")]
        public string PropertyName { get; set; } 

        [JsonPropertyName("beforeValue")]
        public string BeforeValue { get; set; } 

        [JsonPropertyName("afterValue")]
        public string AfterValue { get; set; } 

        [JsonPropertyName("changeCategory")]
        public string ChangeCategory { get; set; } 

        [JsonPropertyName("changeType")]
        public string ChangeType { get; set; } 
    }

    public class Change    {
        [JsonPropertyName("changeId")]
        public string ChangeId { get; set; } 

        [JsonPropertyName("beforeSnapshot")]
        public BeforeSnapshot BeforeSnapshot { get; set; } 

        [JsonPropertyName("afterSnapshot")]
        public AfterSnapshot AfterSnapshot { get; set; } 

        [JsonPropertyName("propertyChanges")]
        public List<PropertyChanx> PropertyChanges { get; set; } 

        [JsonPropertyName("changeType")]
        public string ChangeType { get; set; } 
    }

    public class RootChanges    {
        [JsonPropertyName("changes")]
        public List<Change> Changes { get; set; } 
    }

}