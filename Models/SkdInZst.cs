using System;
using System.Collections.Generic;

namespace Medox.Models;

public partial class SkdInZst
{
    public int ZstId { get; set; }

    public int ZstMainId { get; set; }

    public string ZstMainNazwa { get; set; } = null!;

    public int SkdId { get; set; }

    public string SkdNazwa { get; set; } = null!;

    public byte ZstSkladIloscSkd { get; set; }
}
