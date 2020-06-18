def BUILDVERSION = sh(script: "echo `date +%s`", returnStdout: true).trim()
properties([parameters([string(defaultValue: '0', description: 'give the build number', name: 'select_build', trim: false)])])

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
   
    stage('Code Deployment'){
      sh label: '', script: 'ansible-playbook /opt/deployment/sync-deployment.yml -i /opt/deployment/hosts'
   }

}
