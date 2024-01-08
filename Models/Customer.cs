using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLKhachHang.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng điền dầy đủ thông tin.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Vui lòng điền dầy đủ thông tin.")]
        public string? Address { get; set; }
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Vui lòng điền dầy đủ thông tin.")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? Phonenumber { get; set; }
    }
}
