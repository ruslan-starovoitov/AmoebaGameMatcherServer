﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;
using Services.Experimental;
using Services.Services.Lootbox;

namespace AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby
{
    [Route("[controller]")]
    [ApiController]
    public class LootboxController:ControllerBase
    {
        private readonly LootboxFacadeService lootboxFacadeService;

        public LootboxController(LootboxFacadeService lootboxFacadeService)
        {
            this.lootboxFacadeService = lootboxFacadeService;
        }
        
        [Route(nameof(BuyLootbox))]
        [HttpPost]
        public async Task<ActionResult<string>> BuyLootbox([FromForm] string playerServiceId)
        {
            if (string.IsNullOrEmpty(playerServiceId))
            {
                return BadRequest();
            }

            LootboxModel lootboxModel = await lootboxFacadeService.CreateLootboxModelAsync(playerServiceId);
            if (lootboxModel == null)
            {
                return BadRequest();
            }
            
            return lootboxModel.SerializeToBase64String();
        }
    }
}