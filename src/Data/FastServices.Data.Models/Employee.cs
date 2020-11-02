﻿namespace HomeServices.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FastServices.Data.Models;
    using HomeServices.Data.Models.Enumerators;

    public class Employee
    {
        public Employee()
        {
            this.Id = Guid.NewGuid().ToString();
            this.EmployeeOrders = new HashSet<EmployeeOrder>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public ICollection<EmployeeOrder> EmployeeOrders { get; set; }
    }
}
