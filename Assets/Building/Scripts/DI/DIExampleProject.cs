﻿using UnityEngine;

namespace DI
{
  public class MyAwesomeProjectService
  {
  }

  public class MySceneService
  {
    private readonly MyAwesomeProjectService _myAwesomeProjectService;

    public MySceneService(MyAwesomeProjectService myAwesomeProjectService) =>
      _myAwesomeProjectService = myAwesomeProjectService;
  }

  public class MyAwesomeFactory
  {
    public MyAwesomeObject CreateInstance(string id, int par1) =>
      new MyAwesomeObject(id, par1);
  }

  public class MyAwesomeObject
  {
    private readonly string _id;
    private readonly int _par1;

    public MyAwesomeObject(string id, int par1)
    {
      _id = id;
      _par1 = par1;
    }

    public override string ToString() =>
      $"Object with id: {_id} and par2: {_par1}";
  }

  public class DIExampleProject : MonoBehaviour
  {
    private void Awake()
    {
      DIContainer projectContainer = new DIContainer();

      projectContainer.RegisterSingleton(_ => new MyAwesomeProjectService());
      projectContainer.RegisterSingleton("option 1", _ => new MyAwesomeProjectService());
      projectContainer.RegisterSingleton("option 2", _ => new MyAwesomeProjectService());

      DIExampleScene sceneRoot = FindObjectOfType<DIExampleScene>();
      sceneRoot.Init(projectContainer);
    }
  }
}
