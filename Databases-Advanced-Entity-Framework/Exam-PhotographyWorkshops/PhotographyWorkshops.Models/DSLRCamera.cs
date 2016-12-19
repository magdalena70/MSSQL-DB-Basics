using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyWorkshops.Models
{
    [Table("DSLRCameras")]
    public class DSLRCamera : Camera
    {
        public int MaxShutterSpeed { get; set; }
    }
}
