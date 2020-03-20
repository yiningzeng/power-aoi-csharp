; �ű��� Inno Setup �ű��� ���ɣ�
; �йش��� Inno Setup �ű��ļ�����ϸ��������İ����ĵ���

#define MyAppName "�����"
#define MyAppVersion "v2.3.10"
#define MyAppPublisher "���������Ӿ��Ƽ����޹�˾"
#define MyAppURL "http://www.qtingvision.com/"
#define MyAppExeName "power-aoi.exe"

[Setup]
; ע: AppId��ֵΪ������ʶ��Ӧ�ó���
; ��ҪΪ������װ����ʹ����ͬ��AppIdֵ��
; (�����µ�GUID����� ����|��IDE������GUID��)
AppId={{4A8BA0BA-CFEF-4D88-A03C-73441B1D959A}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={localappdata}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputBaseFilename=����˰�װ��{#MyAppVersion}
SetupIconFile=D:\power-aoi-csharp\power-aoi\aa.ico
Compression=lzma
SolidCompression=yes
PrivilegesRequired=none

[Languages]
Name: "chinesesimp"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\power-aoi.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\config.ini"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\aoi.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\bad_marker.jpg"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\default.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\DockPanel.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Elasticsearch.Net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Elasticsearch.Net.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Elasticsearch.Net.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Emgu.CV.UI.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Emgu.CV.UI.GL.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Emgu.CV.UI.GL.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Emgu.CV.UI.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Emgu.CV.World.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Emgu.CV.World.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\EntityFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\EntityFramework.SqlServer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\EntityFramework.SqlServer.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\EntityFramework.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.Abstractions.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.Abstractions.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.Commands.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.Commands.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.Commands.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.FileSystem.DotNet.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.FileSystem.DotNet.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.FileSystem.DotNet.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\FubarDev.FtpServer.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\ImageProcessor.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\ImageProcessor.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\KdTreeLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\log4net.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\log4net.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\marker.jpg"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\MetroFramework.Design.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\MetroFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\MetroFramework.Fonts.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.AspNetCore.Http.Features.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.AspNetCore.Http.Features.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Bcl.AsyncInterfaces.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Bcl.AsyncInterfaces.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Diagnostics.Tracing.EventSource.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Diagnostics.Tracing.EventSource.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.DotNet.PlatformAbstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.DependencyInjection.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.DependencyInjection.Abstractions.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.DependencyInjection.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.DependencyInjection.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.DependencyModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.Logging.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.Logging.Abstractions.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.Options.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.Options.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Microsoft.Extensions.Primitives.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\MySql.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\MySql.Data.Entity.EF6.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\MySql.Data.Entity.EF6.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\MySql.Data.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Nest.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Nest.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Nest.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\NGettext.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\NGettext.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\opencv_world420.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\OpenTK.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\OpenTK.GLControl.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\OpenTK.GLControl.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\OpenTK.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\power-aoi.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\power-aoi.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\power-aoi.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\RabbitMQ.Client.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\RabbitMQ.Client.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\RTree.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Scrutor.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\Scrutor.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\SmartThreadPool.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Buffers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Buffers.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.ComponentModel.Annotations.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Diagnostics.DiagnosticSource.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Diagnostics.DiagnosticSource.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.IO.Pipelines.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.IO.Pipelines.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Memory.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Numerics.Vectors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Numerics.Vectors.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Runtime.CompilerServices.Unsafe.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Text.Encoding.CodePages.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Text.Encoding.CodePages.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Threading.Channels.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Threading.Channels.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Threading.Tasks.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\System.Threading.Tasks.Extensions.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\WeifenLuo.WinFormsUI.Docking.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\WeifenLuo.WinFormsUI.Docking.ThemeVS2015.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\ZedGraph.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\ZedGraph.xml"; DestDir: "{app}"; Flags: ignoreversion  
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\de\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\es\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\fr\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\hu\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\it\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\ja\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\de\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\de\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\zh-cn\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\x86\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "D:\power-aoi-csharp\power-aoi\bin\Release\x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; ע��: ��Ҫ���κι���ϵͳ�ļ���ʹ�á�Flags: ignoreversion��

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

