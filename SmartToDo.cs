

public class SmartToDo{
  private List<Task> tasks;

  private Interval Morning;   //Default: Morning(7-12)
  private Interval Afternoon; //Default: Afternoon(12-17)
  private Interval Evening;   //Default: Evening(17-22)

  private float allowedOffset;

  //private List<Interval> UsedTime;

  public SmartToDo(float allowedOffset = 5f){
    tasks = new List<Task>();

    Morning   = new Interval(new DateTime(1,1,1, 7,0,0), new DateTime(1,1,1,12,0,0));
    Afternoon = new Interval(new DateTime(1,1,1,12,0,0), new DateTime(1,1,1,17,0,0));
    Evening   = new Interval(new DateTime(1,1,1,17,0,0), new DateTime(1,1,1,22,0,0));

    this.allowedOffset = allowedOffset;

    //UsedTime = new List<Interval>();
  }

  public void AddTask(Task newTask){ //SHOULD BE PRIVATE
    //Check reccomended
    tasks.Add(newTask);
    SortTasks();
    //UsedTime = CalcUsedTime(new Interval(DateTime.Today, new TimeSpan(30,0,0)));
  }

  public List<Interval> newTaskCheck(Task newTask){
    return CalcUsedTime(newTask.GetTime());
    //RETURNS list of USED times in preffered time if exists
  }  

  public List<Interval> newTaskCheck(Task newTask, Interval preferedInterval){
    var R = CalcFreeTime(preferedInterval);
    var r = new List<Interval>();
    foreach(var i in R){
      if(i.GetDuration() > (newTask.GetTime().GetDuration() * (1f-allowedOffset/100f))){
        r.Add(i);
      }
    }
    return r;
    //RETURNS list of FREE times in prefferedInterval which can fit newTask
  }  

  private List<Interval> CalcUsedTime(Interval period){
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

  private List<Interval> CalcFreeTime(Interval period){
    return Interval.Invert(CalcUsedTime(period), period);
  }

  public void SortTasks(bool asc = true){
    //times.Sort();  TODO research  
    for(int i = 0; i<tasks.Count()-1; i++){
      for(int j = i+1; j<tasks.Count(); j++){
        if(tasks[i].GetTime().GetStartTime()>tasks[j].GetTime().GetStartTime() == asc){
          var T = tasks[i];
          tasks[i] = tasks[j];
          tasks[j] = T;
        }
      }
    }
  }
}