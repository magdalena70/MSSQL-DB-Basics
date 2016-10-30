using MiniORM.Attributes;
using System;

namespace MiniORM.Entities
{
    [Entity(TableName = "Users")]
    class User
    {
        [Id]
        private int id;

        [Column(ColumnName = "Username")]
        private string username;

        [Column(ColumnName = "Pass")]
        private string password;

        [Column(ColumnName = "Age")]
        private int age;

        [Column(ColumnName = "RegistrationDate")]
        private DateTime registratinDate;

        [Column(ColumnName = "LastLoginTime")]
        private DateTime lastLoginTime;

        [Column(ColumnName = "IsActive")]
        private bool isActive;

        public User(string username, string password, int age, DateTime registratinDate, DateTime lastLoginTime, bool isActive)
        {
            this.Username = username;
            this.Password = password;
            this.Age = age;
            this.RegistratinDate = registratinDate;
            this.LastLoginTime = lastLoginTime;
            this.IsActive = isActive;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }

        public DateTime RegistratinDate
        {
            get
            {
                return registratinDate;
            }

            set
            {

                registratinDate = value;
            }
        }

        public DateTime LastLoginTime
        {
            get
            {
                return this.lastLoginTime;
            }

            set
            {
                this.lastLoginTime = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }

            set
            {
                this.isActive = value;
            }
        }
    }
}
