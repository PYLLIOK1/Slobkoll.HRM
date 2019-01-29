using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slobkoll.HRM.Web.Models
{
    public class SubTaskModelEdit
    {
        public int Id { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}