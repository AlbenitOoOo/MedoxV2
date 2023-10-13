using System;
using System.Collections.Generic;

namespace Medox.Models;

public partial class KorSkd
{
    /// <summary>
    /// Identyfikator reprezentacji skladnika.
    /// </summary>
    public int SkdId { get; set; }

    /// <summary>
    /// Nazwa typu składnika.
    /// </summary>
    public string SkdNazwa { get; set; } = null!;

    /// <summary>
    /// Opis typu składnika.
    /// </summary>
    public string? SkdOpis { get; set; }

    public bool SkdCzyAktywny { get; set; }

    /// <summary>
    /// Data pierwszego zapisu do bazy
    /// </summary>
    public DateTime DbDataWpisu { get; set; }

    /// <summary>
    /// Osoba dokonująca pierwszego zapisu do bazy
    /// </summary>
    public short DbPerId { get; set; }

    public virtual ICollection<KorZstSklad> KorZstSklads { get; set; } = new List<KorZstSklad>();
}
