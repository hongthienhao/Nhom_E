using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace KoiDeliveryOrderingSystem.Repositories
{
    public class FeedbackReport
    {
        public string ServiceName { get; set; }
        public int TotalFeedbacks { get; set; }
        public double AverageRating { get; set; }
    }
}
