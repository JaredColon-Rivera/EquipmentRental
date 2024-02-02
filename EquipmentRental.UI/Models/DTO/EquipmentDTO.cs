﻿namespace EquipmentRental.UI.Models.DTO
{
    public class EquipmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Rented { get; set; }

        public CustomerDTO? Customer { get; set; }
        public LocationDTO Location { get; set; }

    }
}
