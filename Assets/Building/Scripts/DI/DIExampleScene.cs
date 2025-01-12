using UnityEngine;

namespace Building.Scripts.DI
{
  public class DIExampleScene : MonoBehaviour
  {
    public void Init(DIContainer projectContainer)
    {
      // MyAwesomeProjectService serviceWithoutTag = projectContainer.Resolve<MyAwesomeProjectService>();
      // MyAwesomeProjectService service1 = projectContainer.Resolve<MyAwesomeProjectService>("option 1");
      // MyAwesomeProjectService service2 = projectContainer.Resolve<MyAwesomeProjectService>("option 2");

      DIContainer sceneContainer = new DIContainer(projectContainer);
      sceneContainer.RegisterSingleton(c => new MySceneService(c.Resolve<MyAwesomeProjectService>()));
      sceneContainer.RegisterSingleton(c => new MyAwesomeFactory());
      sceneContainer.RegisterInstance(new MyAwesomeObject("instance", 10));
      
      MyAwesomeFactory objectFactory = sceneContainer.Resolve<MyAwesomeFactory>();
      for (int i = 0; i < 3; i++)
      { 
        string id = $"o{i}";
        MyAwesomeObject o = objectFactory.CreateInstance(id, i);
        Debug.Log($"Object created with factory. \n{o}");
      }
      
      MyAwesomeObject instance = sceneContainer.Resolve<MyAwesomeObject>();
      Debug.Log($"Object instance. \n{instance}");
    }
  }
}
