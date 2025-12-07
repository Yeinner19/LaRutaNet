using System;
using System.Collections.Generic;

namespace LaRutaNet.Models;

public partial class UserFitnessHistory
{
    public long Id { get; set; }

    public double? ArmMeasurement { get; set; }

    public double? BodyFatPercentage { get; set; }

    public double? ChestMeasurement { get; set; }

    public double? HipMeasurement { get; set; }

    public DateOnly? RecordDate { get; set; }

    public double? ThighMeasurement { get; set; }

    public double? WaistMeasurement { get; set; }

    public double? Weight { get; set; }

    public long UserId { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual User User { get; set; } = null!;
}
