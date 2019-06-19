using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NBandServer
{
    [Table("FileMissionTable")]
    public class FileMission
    {
        public int ID { get; set; }
        public string DateTime { get; set; }
    }
}
