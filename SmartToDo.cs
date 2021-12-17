

public class SmartToDo{
  private List<Task> tasks;

  private Interval Morning;   //Default: Morning(7-12)
  private Interval Afternoon; //Default: Afternoon(12-17)
  private Interval Evening;   //Default: Evening(17-22)

  private List<Interval> UsedTime;

  public SmartToDo(){
    tasks = new List<Task>();

    Morning   = new Interval(new DateTime(1,1,1, 7,0,0), new DateTime(1,1,1,12,0,0));
    Afternoon = new Interval(new DateTime(1,1,1,12,0,0), new DateTime(1,1,1,17,0,0));
    Evening   = new Interval(new DateTime(1,1,1,17,0,0), new DateTime(1,1,1,22,0,0));

    UsedTime = new List<Interval>();
  }

  public void AddTask(Task newTask){
    
  }

  public List<Interval> newTaskCheck(Task newTask, Interval preferedInterval){
    return new List<Interval>();//delete
    //TODO implement
  }  

  private List<Interval> FindUsedTime(Interval period){
    List<Interval> R = new List<Interval>();
    for(int i = 0; i<tasks.Count(); i++){
      var newTimes = tasks[i].UsedTime(period);
      foreach(var time in newTimes){
        R.Add(time);
      }
    }
    R = Interval.SortByStartTime(R);
    return R;
  }

  private List<Interval> FindFreeTime(Interval period){
    return Interval.Invert(Interval.Crop(UsedTime, period), period);
  }
}