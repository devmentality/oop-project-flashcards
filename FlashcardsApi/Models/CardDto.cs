﻿using System;

namespace FlashcardsApi.Models
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public Guid CollectionId { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}
