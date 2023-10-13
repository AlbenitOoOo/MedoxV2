using System;
using System.Collections.Generic;

namespace Medox.Models;

public partial class KorZstToKorZstSklad
{
    /// <summary>
    /// Identyfikator modyfikacji składu zestawu
    /// </summary>
    public int ZstToZstSkladId { get; set; }

    /// <summary>
    /// Data modyfikacji składu zestawu
    /// </summary>
    public DateTime ZstToZstSkladData { get; set; }

    /// <summary>
    /// Identyfikator osoby dokonującej modyfikacji składu zestawu
    /// </summary>
    public int PerId { get; set; }

    public virtual ICollection<KorZstSklad> KorZstSklads { get; set; } = new List<KorZstSklad>();

    public virtual ICollection<KorZst> KorZsts { get; set; } = new List<KorZst>();
}
