//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartWorkoutsWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkoutElements
    {
        public int ID_Element { get; set; }
        public int ID_Exercises { get; set; }
        public int ID_Workout { get; set; }
    
        public virtual Exercises Exercises { get; set; }
        public virtual Workouts Workouts { get; set; }
    }
}
