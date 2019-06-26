using SQLite4Unity3d;

public class wga_hu {

    [PrimaryKey]
    public string filename { get; set; }
    public float hue_mean { get; set; }
    public int hue_median { get; set; }
    public float hue_stddev { get; set; }
    public float saturation_mean { get; set; }
    public int saturation_median { get; set; }
    public float saturation_stddev { get; set; }
    public float brightness_mean { get; set; }
    public int brightness_median { get; set; }
    public float brightness_stddev { get; set; }
    public string form { get; set; }
    public string type { get; set; }
    public string school { get; set; }
    public string timeframe { get; set; }
    public string author { get; set; }
    public string borndied { get; set; }
    public string title { get; set; }
    public string date { get; set; }
    public string technique { get; set; }
    public string location { get; set; }
    public float hue_entropy { get; set; }
    public float saturation_entropy { get; set; }
    public float brightness_entropy { get; set; }
    public int date_num { get; set; }

    public override string ToString()
    {
        return string.Format("[registro: filename={0}, hue_mean={1},  hue_median={2}]", filename, hue_mean, hue_median);
    }

}