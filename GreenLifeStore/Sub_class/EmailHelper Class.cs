using System.Windows.Forms;

namespace GreenLifeStore.Sub_class
{
    public static class EmailHelper
    {
        // Simulate email confirmation
        public static void SendPasswordResetConfirmation(string email)
        {
            MessageBox.Show(
                "Password reset successful for: " + email +
                "\n\nYou can now log in with your new password.",
                "Password Reset Confirmation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}