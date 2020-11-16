using System;
using System.Collections.Generic;

namespace ChangeHistoryWebApp
{

  public class ChangeProperties    {

    
        public string ResourceId { get; set; } 
        public string PropertyName { get; set; } 
        public string BeforeValue { get; set; } 
        public string AfterValue { get; set; } 
        public string ChangeCategory { get; set; } 
        public string Timestamp { get; set; }

    }
}