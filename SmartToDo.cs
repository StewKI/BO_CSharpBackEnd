

public class SmartToDo{
  List<Task> tasks = new List<Task>();

  Interval Morning   = new Interval(new DateTime(1,1,1, 7,0,0), new DateTime(1,1,1,12,0,0));
  Interval Afternoon = new Interval(new DateTime(1,1,1,12,0,0), new DateTime(1,1,1,17,0,0)); 
  Interval Evening   = new Interval(new DateTime(1,1,1,17,0,0), new DateTime(1,1,1,22,0,0));
  //Default: Morning(7-12), Afternoon(12-17), Evening(17-22)

  public List<Interval> newTaskCheck(Task newTask, Interval preferedInterval){
    return new List<Interval>();//delete
    //TODO implement
  }  


  public void AddTask(Task newTask){
    //TODO implement
  }

  private List<Interval> UsedTime(Interval period){
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

  private List<Interval> FreeTime(Interval period){
    return Interval.Invert(UsedTime(period), period);
  }
}