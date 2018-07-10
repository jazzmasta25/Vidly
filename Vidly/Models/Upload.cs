using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Vidly.Models
{
    public class Upload
    {
        [Required(ErrorMessage = "Please select file.")]
        public HttpPostedFileBase File { get; set; }

        [FileExtensions(Extensions = "csv", ErrorMessage = "Only CSV Files allowed.")]
        public string FileName
        {
            get
            {
                if (File != null)
                    return File.FileName;
                else
                    return "";
            }
        }

        public int Processed { get; set; }

        public string ColumnError { get; set; }


    }
}