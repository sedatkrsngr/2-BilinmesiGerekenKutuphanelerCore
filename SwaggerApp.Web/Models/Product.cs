using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SwaggerApp.Web.Models
{
    public partial class Product
    {
        /// <summary>
        /// Ürün ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ürün Adı
        /// </summary>
        [Required]//bu sayede kırmızı * gösterir
        public string Name { get; set; }
        /// <summary>
        /// Ürün Tutarı
        /// </summary>
        /// 
        [Required]
        public decimal? Price { get; set; }
        /// <summary>
        /// Ürün EklenmeTarihi
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Ürün Kategorisi
        /// </summary>
        public string Category { get; set; }
    }
}
