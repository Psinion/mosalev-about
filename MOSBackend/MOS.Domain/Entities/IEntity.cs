﻿namespace MOS.Domain.Entities;

public interface IEntity<TKey> where TKey : struct
{
    TKey Id { get; set; }
}