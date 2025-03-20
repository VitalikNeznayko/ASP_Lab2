using ClinicBooking.Models;

namespace ClinicBooking.Models.ViewModels
{
    public class ViewModel<T> where T : class
    {
        public IEnumerable<T> Collections { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}