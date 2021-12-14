
public class Task
{
  protected string title;
  protected string desc;

  protected Interval time;


  public Interval? Intersect(Task other){
    return time.Intersect(other.time);
  }

#region constructors
  public Task(){
    this.title = "New Task";
    this.desc = "Generic Task Generated By An Empty Constructor";
    this.time = new Interval();
  }

  public Task(string title, string desc, Interval time){
    this.title = title;
    this.desc = desc;
    this.time = time;
  }

  public override string ToString()
  {
    return title + " " + desc;
  }
#endregion
}

public class Routine : Task
{
  protected bool[] repeatDays;

  public Routine(){
    this.title = "New Routine";
    this.desc = "Generic Routine Generated By An Empty Constructor";
    this.time = new Interval();
    repeatDays = new bool[7];
  }

  public Routine(string title, string desc, Interval time, bool[] repeatDays){
    this.title = title;
    this.desc = desc;
    this.time = time;
    this.repeatDays = repeatDays;
  }

  //TODO IMPLEMENT MORE...
}

public class AdvancedRoutine : Routine{
  protected int repeatWeeks; //Every 2nd, every 3rd week etc.
  protected int weekOffset; // between 0 and (repeatWeeks-1)

  public AdvancedRoutine(){
    this.title = "New AdvancedRoutine";
    this.desc = "Generic AdvancedRoutine Generated By An Empty Constructor";
    this.time = new Interval();
    this.repeatDays = new bool[7];
    this.repeatWeeks = 2;
    this.weekOffset = 0;
  }

  public AdvancedRoutine(string title, string desc, Interval time, bool[] repeatDays, int repeatWeeks, int weekOffset){
    this.title = title;
    this.desc = desc;
    this.time = time;
    this.repeatDays = repeatDays;
    this.repeatWeeks = repeatWeeks;
    this.weekOffset = weekOffset;
  }

  //TODO IMPLEMENT MORE...
}