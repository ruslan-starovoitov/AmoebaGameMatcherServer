﻿using System;
using System.Threading.Tasks;
using Libraries.NetworkLibrary.Experimental;
using Microsoft.AspNetCore.Mvc;
using Services.Experimental;
using Services.Services.MatchFinishing;

namespace AmoebaGameMatcherServer.Controllers.ProfileServer
{
    /// <summary>
    /// Принимает запросы на результат конкретного боя.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class MatchResultController : ControllerBase
    {
        private readonly IPlayerMatchResultDbReaderService matchResultDbReaderService;

        public MatchResultController(IPlayerMatchResultDbReaderService matchResultDbReaderService)
        {
            this.matchResultDbReaderService = matchResultDbReaderService;
        }
        
        /// <summary>
        /// Получение результата матча для показа статистики после боя.
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="playerServiceId"></param>
        /// <returns></returns>
        [Route(nameof(Get))]
        [HttpGet]
        public async Task<ActionResult<string>> Get([FromQuery] int? matchId, [FromQuery] string playerServiceId)
        {
            if (matchId == null)
            {
                Console.WriteLine($"{nameof(matchId)} is null");
                return BadRequest();
            }

            if (playerServiceId == null)
            {
                Console.WriteLine($"{nameof(playerServiceId)} is null");
                return BadRequest();
            }
            
            MatchResultDto matchResultDto = await matchResultDbReaderService
                .ReadMatchResultAsync(matchId.Value, playerServiceId);
            
            
            if (matchResultDto == null)
            {
                throw new NullReferenceException(nameof(matchResultDto));
            }

            if (matchResultDto.CurrentWarshipRating < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(matchResultDto.CurrentWarshipRating));
            }

            if (matchResultDto.LootboxPoints.Count == 0)
            {
                throw new Exception("список наград пуст");
            }
            
            foreach (var pair in matchResultDto.LootboxPoints)
            {
                Console.WriteLine($"{pair.Key} {pair.Value}");
            }
            
            
            //Чек на адекватность ответа
            if (matchResultDto == null)
            {
                return StatusCode(500);
            }

            if (matchResultDto.CurrentWarshipRating < 0)
            {
                Console.WriteLine($"\n\n\n\n{nameof(matchResultDto.CurrentWarshipRating)} {matchResultDto.CurrentWarshipRating }");
                return StatusCode(500);
            }
            
            return matchResultDto.SerializeToBase64String();
        }
    }
}