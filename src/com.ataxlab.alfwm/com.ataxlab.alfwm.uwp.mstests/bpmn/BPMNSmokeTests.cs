using com.ataxlab.alfwm.model.bpmn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace com.ataxlab.alfwm.uwp.mstests.bpmn
{
    [TestClass]
    public class BPMNSmokeTests
    {
        [TestMethod]
        public async Task CanLoadSampleBPMN()
        {
            StorageFile sampleFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///samplebpmn.xml"));

            var serialzer = new XmlSerializer(typeof(tDefinitions));
            var XmlStream = new StreamReader(sampleFile.Path);
            var document = (tDefinitions)serialzer.Deserialize(XmlStream);
            var process = document.process;
            var sequenceFlow = process[0].sequenceFlow[0];
            int i = 0;
        }
    }
}
