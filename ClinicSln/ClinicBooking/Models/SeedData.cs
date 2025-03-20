using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using System;
using System.Linq;

namespace ClinicBooking.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Pacients.Any() && !context.Doctors.Any() && !context.Appointments.Any())
                {
                    var pacients = new[]
                    {
                        new Pacient { FullName = "Іван Петренко", DateOfBirth = new DateTime(1985, 5, 12), PhoneNumber = "+380981234567", Email = "ivan.petrenko@gmail.com", Address = "вул. Шевченка, 10", MedicalHistory = "Алергія на пил" },
                        new Pacient { FullName = "Марія Іваненко", DateOfBirth = new DateTime(1992, 8, 25), PhoneNumber = "+380679876543", Email = "maria.ivanenko@gmail.com", Address = "вул. Лесі Українки, 5", MedicalHistory = "Гіпертонія" },
                        new Pacient { FullName = "Олег Сидоренко", DateOfBirth = new DateTime(1978, 3, 30), PhoneNumber = "+380503219876", Email = "oleg.sydorenko@gmail.com", Address = "вул. Франка, 15", MedicalHistory = "Діабет" },
                        new Pacient { FullName = "Анна Ковальчук", DateOfBirth = new DateTime(2001, 11, 8), PhoneNumber = "+380637894561", Email = "anna.kovalchuk@gmail.com", Address = "вул. Грушевського, 20", MedicalHistory = "Мігрень" },
                        new Pacient { FullName = "Юрій Мельник", DateOfBirth = new DateTime(1990, 6, 17), PhoneNumber = "+380731122334", Email = "yuriy.melnyk@gmail.com", Address = "вул. Перемоги, 7", MedicalHistory = "Хронічний бронхіт" }
                    };

                    context.Pacients.AddRange(pacients);
                    context.SaveChanges();

                    var doctors = new[]
                    {
                        new Doctor { DoctorName = "Андрій Коваленко", Specialization = "Кардіолог", PhoneNumber = "+380992233445", Email = "andriy.kovalenko@gmail.com", Cabinet = "Кабінет 12"},
                        new Doctor {DoctorName = "Олена Ткаченко", Specialization = "Терапевт", PhoneNumber = "+380735566778", Email = "olena.tkachenko@gmail.com", Cabinet = "Кабінет 7"},
                        new Doctor {DoctorName = "Микола Василенко", Specialization = "Невролог", PhoneNumber = "+380679988776", Email = "mykola.vasylenko@gmail.com", Cabinet = "Кабінет 5"}
                    };

                    context.Doctors.AddRange(doctors);
                    context.SaveChanges();

                    var appointments = new[]
                    {
                        new Appointment { PacientID = pacients[0].PacientID, DoctorID = doctors[0].DoctorID, AppointmentDate = DateTime.Now.AddDays(2), Notes = "Перевірка серцево-судинної системи" },
                        new Appointment { PacientID = pacients[1].PacientID, DoctorID = doctors[1].DoctorID, AppointmentDate = DateTime.Now.AddDays(4), Notes = "Щорічний медогляд" },
                        new Appointment { PacientID = pacients[2].PacientID, DoctorID = doctors[2].DoctorID, AppointmentDate = DateTime.Now.AddDays(1), Notes = "Консультація щодо головного болю" },
                        new Appointment { PacientID = pacients[3].PacientID, DoctorID = doctors[0].DoctorID, AppointmentDate = DateTime.Now.AddDays(5), Notes = "Контрольний візит після лікування" },
                        new Appointment { PacientID = pacients[4].PacientID, DoctorID = doctors[1].DoctorID, AppointmentDate = DateTime.Now.AddDays(3), Notes = "Огляд через проблеми з диханням" },
                        new Appointment { PacientID = pacients[1].PacientID, DoctorID = doctors[2].DoctorID, AppointmentDate = DateTime.Now.AddDays(6), Notes = "Діагностика нервових розладів" },
                        new Appointment { PacientID = pacients[0].PacientID, DoctorID = doctors[2].DoctorID, AppointmentDate = DateTime.Now.AddDays(7), Notes = "Призначення лікування" },
                        new Appointment { PacientID = pacients[3].PacientID, DoctorID = doctors[1].DoctorID, AppointmentDate = DateTime.Now.AddDays(8), Notes = "Повторна консультація" }
                    };

                    context.Appointments.AddRange(appointments);
                    context.SaveChanges();
                }
            }
        }
    }
}
