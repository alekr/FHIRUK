using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHIRUK.Resources
{
    public class Attachment
    {
        public String contentType { get; set; } //  <!-- 1..1 Mime type of the content, with charset etc. -->
        public String language { get; set; }    //  <!-- 0..1 Human language of the content (BCP-47) -->
        public String data { get; set; }    //  <!-- 0..1 Data inline, base64ed -->
        public Uri url { get; set; }    //  <!-- 0..1 Uri where the data can be found -->
        public Int32 size { get; set; } //  <!-- 0..1 Number of bytes of content (if url provided) -->
        public String hash { get; set; }    //  <!-- 0..1 Hash of the data (sha-1, base64ed ) -->
        public String title { get; set; }   //  <!-- 0..1 Label to display in place of the data -->
    }

    public class Attachments : List<Attachment>
    {
        /// <summary>
        /// Todo: may not need this method - how do you return multiple attachments in a string? return a list of titles?
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            if (this.Count == 0)
                return String.Empty;
            else
                return "<attachements>";
        }
    }
}
