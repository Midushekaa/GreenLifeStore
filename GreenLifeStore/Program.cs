using System;
using System.Windows.Forms;
using GreenLifeStore.Sub_class; // For User class if needed

namespace GreenLifeStore
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Start the application with LoginForm
            //Comment this out if you want to start dashboards directly
            Application.Run(new LoginForm());

          
           // User adminUser = new User(
           //    1,
           //    "Admin User",
           //    "admin@gmail.com",
           //    User.HashPassword("123"),
           //    "ADMIN"
           //);

           // User customerUser = new User(
           //     2,
           //     "Customer User",
           //     "customer@gmail.com",
           //     User.HashPassword("123"),
           //     "CUSTOMER"
           // );
           // bool startAsAdmin = true; // Change to false to start as Customer

           // if (startAsAdmin)
           // {
           //     Application.Run(new AdminDashboard(adminUser));
           // }
           // else
           // {
           //     Application.Run(new CustomerDashboard(customerUser));
           // }
        }
    }
}