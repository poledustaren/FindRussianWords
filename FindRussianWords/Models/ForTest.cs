using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindRussianWords.Models
{
    public class ForTest
    {
        public string StringForTest { get; set; }
        public string StringResultForTest { get; set; }

        public void OperationWithString()
        {
            this.StringResultForTest = this.StringForTest + "-Now this is shitty result string";
        }
    }
}