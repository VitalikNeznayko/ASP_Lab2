using ClinicBooking.Models;
using Microsoft.AspNetCore.SignalR;

namespace ClinicBooking.Hubs
{
    public class AppointmentHub : Hub
    {
        public async Task SendAppointmentUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveAppointmentUpdate", message);
        }
        public async Task SendAppointmentCreate(Appointment appointment)
        {
            await Clients.All.SendAsync("ReceiveAppointmentCreate", appointment);
        }

        public async Task SendAppointmentEdit(Appointment appointment)
        {
            await Clients.All.SendAsync("ReceiveAppointmentEdit", appointment);
        }

        public async Task SendAppointmentDelete(int appointmentId)
        {
            await Clients.All.SendAsync("ReceiveAppointmentDelete", appointmentId);
        }
    }
}
