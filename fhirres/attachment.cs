using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHIRUK.Resources
{
    public class Attachment
    {
        public String ContentType { get; set; } //  <!-- 1..1 Mime type of the content, with charset etc. -->
        public String Language { get; set; }    //  <!-- 0..1 Human language of the content (BCP-47) -->
        public String Data { get; set; }    //  <!-- 0..1 Data inline, base64ed -->
        public Uri Url { get; set; }    //  <!-- 0..1 Uri where the data can be found -->
        public Int32 Size { get; set; } //  <!-- 0..1 Number of bytes of content (if url provided) -->
        public String Hash { get; set; }    //  <!-- 0..1 Hash of the data (sha-1, base64ed ) -->
        public String Title { get; set; }   //  <!-- 0..1 Label to display in place of the data -->
    }
}
