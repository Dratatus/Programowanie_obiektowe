﻿namespace CrashApp.Data.Entities
{
    public class Player : EntityBase
    {
        /// <summary>
        /// klasa przechowująca właściwości gracza
        /// </summary>
        public string Username { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }

        public virtual int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}