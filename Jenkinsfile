node{
   stage('SCM Checkout'){
     git 'https://github.com/pnewalkar/Sync.git'
   }
   stage('Dotnet Build'){
      sh label: '', script: '''cd Maintel.Icon.Portal.Sync.HighlightAPI
      dotnet build'''
   }
    stage('Dotnet Test'){
      sh label: '', script: '''cd Maintel.Icon.Portal.Sync.HighlightAPI.Spec
      dotnet test'''  
   }

   stage('Zip Artifacts'){
      sh label: '', script: '''cp /var/lib/jenkins/workspace/demo-jenkinsfile-git/Maintel.Icon.Portal.Sync.HighlightAPI/bin/Debug/netcoreapp2.2/* /Artifacts/
      zip -r /Artifacts/Artifacts.zip /Artifacts/'''
   }

}
