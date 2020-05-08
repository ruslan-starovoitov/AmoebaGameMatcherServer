﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    public class Warship
    {
        private readonly ILazyLoader lazyLoader;
        public Warship()
        {
        }
        public Warship(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public int AccountId { get; set; }
        [Required] public int WarshipTypeId { get; set; }
        
        [NotMapped] public int PowerLevel { get; set; }
        [NotMapped] public int PowerPoints { get; set; }
        [NotMapped] public int Rating { get; set; }
        
        [ForeignKey("AccountId")] public virtual  Account Account { get; set; }
        [ForeignKey("WarshipTypeId")] public virtual WarshipType WarshipType { get; set; }
        
        private List<MatchResultForPlayer> matchResultForPlayers;
        public virtual List<MatchResultForPlayer> MatchResultForPlayers 
        {
            get => lazyLoader.Load(this, ref matchResultForPlayers);
            set => matchResultForPlayers = value;
        }
        
        private List<WarshipImprovementPurchase> warshipImprovementPurchases;
        public virtual List<WarshipImprovementPurchase> WarshipImprovementPurchases 
        {
            get => lazyLoader.Load(this, ref warshipImprovementPurchases);
            set => warshipImprovementPurchases = value;
        }
    }
}