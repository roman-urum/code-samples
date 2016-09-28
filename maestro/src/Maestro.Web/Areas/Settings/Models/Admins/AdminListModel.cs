using System;

namespace Maestro.Web.Areas.Settings.Models.Admins
{
    public class AdminListModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Phone { get; set; }

        public bool IsEnabled { get; set; }

        public string Role { get; set; }

    }
}