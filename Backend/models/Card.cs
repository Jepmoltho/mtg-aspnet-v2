using System;
using System.Collections.Generic;

public class Card
{
    public int CardId { get; set; }

    public string? MtgCardId { get; set; }
    public string? Title { get; set; }
    public string? Set { get; set; }
    public string? SuperType { get; set; }
    public string? imgUrl { get; set; }

    public int UserId { get; set; }
    //public User User { get; set; }
}
