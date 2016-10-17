# CubeXrmFramework
Framework for Microsoft Dynamics CRM 2013, 2015 and 2016

Cube.XRM.Framework.Core
  This is the main project of this framework. If you want to integrate external applications with Dynamics CRM you have to generate a Service object and connect to the Dynamics CRM. This project generates Dynamics CRM Service and connects to the CRM instance. Also this project contains some other core functionalities of framework such as logging.

Cube.XRM.Framework
  All framework functionalities are coming from this project. 
  •	Cube Base: All main functionalities are collected in this class like Plugin/Workflow Context, Create, Retrieve, Update and Delete operations, etc.
  •	Cube Entity: If you want to create your own classes like Dynamics CRM entities this class will help you with add you some extra functionalities if you inherit your class from this class.
  •	Exception Handler: Handle CRM Exceptions
  •	Object Carrier: You can keep objects in memory with specified keys and access them later. 
  •	Settings: Framework setting are generating from this class.

Cube.XRM.Framework.AddOn
  You can access all framework functionalities - Context, Log Mechanism, Service will be ready to use in your Plugin and Workflow.

Cube.XRM.Framework.IntegrationTester
  This is just a test project also you can generate your settings file with this project.

Cube.XRM.Workflow.HelperTool
  This is a custom workflow that is accessible from your workflow screen while you create a workflow. It is include some useful functionalities for a workflow.
	

