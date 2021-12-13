

public class SmartToDo{
  List<Task> tasks = new List<Task>();

  Interval Morning   = new Interval(new DateTime(1,1,1, 7,0,0), new DateTime(1,1,1,12,0,0));
  Interval Afternoon = new Interval(new DateTime(1,1,1,12,0,0), new DateTime(1,1,1,17,0,0)); 
  Interval Evening   = new Interval(new DateTime(1,1,1,17,0,0), new DateTime(1,1,1,22,0,0));
  //Default: Morning(7-12), Afternoon(12-17), Evening(17-22)




  public void AddTask(Task newTask){
  }
}