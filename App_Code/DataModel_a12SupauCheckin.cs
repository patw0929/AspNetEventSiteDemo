/***
* Patw.Backend.DataModel_a12SupauCheckin.cs
* ===========================================================
*
***/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace Patw.Backend
{
    [PetaPoco.TableName("a12SupauCheckin")]
    [PetaPoco.PrimaryKey("sID")]
    [PetaPoco.ExplicitColumns]
    public class DataModel_a12SupauCheckin
    {
        
        [PetaPoco.Column]
        public int sID { get; set; }

        [PetaPoco.Column]
        public string sFBUID { get; set; }

        [PetaPoco.Column]
        public string sFBDisplayName { get; set; }

        [PetaPoco.Column]
        public string sName { get; set; }

        [PetaPoco.Column]
        public int sGender { get; set; }

        [PetaPoco.Column]
        public DateTime sBirth { get; set; }

        [PetaPoco.Column]
        public string sFBEmail { get; set; }

        [PetaPoco.Column]
        public string sEmail { get; set; }

        [PetaPoco.Column]
        public string sMobile { get; set; }

        [PetaPoco.Column]
        public int sLocation { get; set; }

        [PetaPoco.Column]
        public string sIP { get; set; }

        [PetaPoco.Column]
        public int sValid { get; set; }

        [PetaPoco.Column]
        public DateTime sCreatetime { get; set; }

        [PetaPoco.Column]
        public int sWin { get; set; }

    }

    public class a12SupauCheckinValidator : AbstractValidator<DataModel_a12SupauCheckin>
    {
        public a12SupauCheckinValidator()
        {
            RuleFor(c => c.sFBUID)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sFBDisplayName)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sName)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sGender)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sBirth)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sFBEmail)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sEmail)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sMobile)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sLocation)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sIP)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sValid)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sCreatetime)
                   .NotEmpty()
                   ;
            RuleFor(c => c.sWin)
                   .NotEmpty()
                   ;

        }
    }
}
