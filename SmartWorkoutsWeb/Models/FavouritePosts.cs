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
    
    public partial class FavouritePosts
    {
        public int IdFavPost { get; set; }
        public int ID_Post { get; set; }
        public int ID_User { get; set; }
    
        public virtual Posts Posts { get; set; }
        public virtual Users Users { get; set; }
    }
}
