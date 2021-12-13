
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
  protected bool[] repeatDays = new bool[7];

  
}

public class AdvancedRoutine : Routine{
  
}