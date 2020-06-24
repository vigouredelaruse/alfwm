using com.ataxlab.alfwm.core.taxonomy.binding;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.library.activity.ocr.tesseract.model
{
    /// <summary>
    /// 
    /// </summary>
    public class TesseractPipelineVariable : IPipelineVariable
    {
        public TesseractPipelineVariable()
        {
            this.ID = Guid.NewGuid().ToString();
            Items = new List<object>();
        }

        public string ID { get; set; }
        public string Key { get; set; }
        public string JsonValue { get; set; }
        public object Payload { get; set; }
        public JSchema JsonValueSchema { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime CreateDate { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public object ParentItem { get; set; }
        public ICollection<object> Items { get; set; }
    }
}
