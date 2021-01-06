﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SquidLeagueAdmin.Models.Enums
{
    public enum Ranks
    {
        [Description("NA")]
        unknown,

        [Description("UN")]
        unranked,

        [Description("C-")]
        cminus,

        [Description("C")]
        c,

        [Description("C+")]
        cplus,

        [Description("B-")]
        bminus,

        [Description("B")]
        b,

        [Description("B+")]
        bplus,

        [Description("A-")]
        aminus,

        [Description("A")]
        a,

        [Description("A+")]
        aplus,

        [Description("S")]
        s,

        [Description("S+")]
        splus,

        [Description("X")]
        x
    }
}
