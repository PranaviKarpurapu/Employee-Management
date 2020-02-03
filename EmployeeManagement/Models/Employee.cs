using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Model
{
   
    public class Employee
    {
        public Employee() { }
        public int Id { get; set; }


       [Required]
       [MaxLength(10,ErrorMessage ="Length can be maximum 10")]
        [Remote(action: "IsNameExist", controller: "Employee")]   //Goes to controller for checking
        public string Name { get; set; }


        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", ErrorMessage = "Please enter a valid email address")]
        [DisplayName("Company Mail")]
        [ValidateDomain(allowdomain:"cts.com")]           //checks valid mail or not 
        [Remote(action:"IsEmailExist",controller:"Employee")]       //Goes to controller for checking
        public string Email { get; set; }
        public Dept Dept { get; set; }

       
        public Employee(int id,string name,string email,Dept dept)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Dept = dept;
        }
    }
}
