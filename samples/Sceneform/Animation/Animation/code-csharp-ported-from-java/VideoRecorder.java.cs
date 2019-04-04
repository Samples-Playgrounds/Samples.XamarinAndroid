// mc++ /*
// mc++  * Copyright 2018 Google LLC
// mc++  *
// mc++  * Licensed under the Apache License, Version 2.0 (the "License");
// mc++  * you may not use this file except in compliance with the License.
// mc++  * You may obtain a copy of the License at
// mc++  *
// mc++  *      http://www.apache.org/licenses/LICENSE-2.0
// mc++  *
// mc++  * Unless required by applicable law or agreed to in writing, software
// mc++  * distributed under the License is distributed on an "AS IS" BASIS,
// mc++  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++  * See the License for the specific language governing permissions and
// mc++  * limitations under the License.
// mc++  */
// mc++ 
// mc++ package com.google.ar.sceneform.samples.animation;
// mc++ 
// mc++ import android.content.res.Configuration;
// mc++ import android.media.CamcorderProfile;
// mc++ import android.media.MediaRecorder;
// mc++ import android.os.Environment;
// mc++ import android.util.Log;
// mc++ import android.util.Size;
// mc++ import android.view.Surface;
// mc++ import com.google.ar.sceneform.SceneView;
// mc++ import java.io.File;
// mc++ import java.io.IOException;
// mc++ 
// mc++ /**
// mc++  * Video Recorder class handles recording the contents of a SceneView. It uses MediaRecorder to
// mc++  * encode the video. The quality settings can be set explicitly or simply use the CamcorderProfile
// mc++  * class to select a predefined set of parameters.
// mc++  */
// mc++ public class VideoRecorder {
// mc++   private static final String TAG = "VideoRecorder";
// mc++   private static final int DEFAULT_BITRATE = 10000000;
// mc++   private static final int DEFAULT_FRAMERATE = 30;
// mc++ 
// mc++   // recordingVideoFlag is true when the media recorder is capturing video.
// mc++   private boolean recordingVideoFlag;
// mc++ 
// mc++   private MediaRecorder mediaRecorder;
// mc++ 
// mc++   private Size videoSize;
// mc++ 
// mc++   private SceneView sceneView;
// mc++   private int videoCodec;
// mc++   private File videoDirectory;
// mc++   private String videoBaseName;
// mc++   private File videoPath;
// mc++   private int bitRate = DEFAULT_BITRATE;
// mc++   private int frameRate = DEFAULT_FRAMERATE;
// mc++   private Surface encoderSurface;
// mc++ 
// mc++   public VideoRecorder() {
// mc++     recordingVideoFlag = false;
// mc++   }
// mc++ 
// mc++   public File getVideoPath() {
// mc++     return videoPath;
// mc++   }
// mc++ 
// mc++   public void setBitRate(int bitRate) {
// mc++     this.bitRate = bitRate;
// mc++   }
// mc++ 
// mc++   public void setFrameRate(int frameRate) {
// mc++     this.frameRate = frameRate;
// mc++   }
// mc++ 
// mc++   public void setSceneView(SceneView sceneView) {
// mc++     this.sceneView = sceneView;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Toggles the state of video recording.
// mc++    *
// mc++    * @return true if recording is now active.
// mc++    */
// mc++   public boolean onToggleRecord() {
// mc++     if (recordingVideoFlag) {
// mc++       stopRecordingVideo();
// mc++     } else {
// mc++       startRecordingVideo();
// mc++     }
// mc++     return recordingVideoFlag;
// mc++   }
// mc++ 
// mc++   private void startRecordingVideo() {
// mc++     if (mediaRecorder == null) {
// mc++       mediaRecorder = new MediaRecorder();
// mc++     }
// mc++ 
// mc++     try {
// mc++       buildFilename();
// mc++       setUpMediaRecorder();
// mc++     } catch (IOException e) {
// mc++       Log.e(TAG, "Exception setting up recorder", e);
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Set up Surface for the MediaRecorder
// mc++     encoderSurface = mediaRecorder.getSurface();
// mc++ 
// mc++     sceneView.startMirroringToSurface(
// mc++         encoderSurface, 0, 0, videoSize.getWidth(), videoSize.getHeight());
// mc++ 
// mc++     recordingVideoFlag = true;
// mc++   }
// mc++ 
// mc++   private void buildFilename() {
// mc++     if (videoDirectory == null) {
// mc++       videoDirectory =
// mc++           new File(
// mc++               Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_PICTURES)
// mc++                   + "/Sceneform");
// mc++     }
// mc++     if (videoBaseName == null || videoBaseName.isEmpty()) {
// mc++       videoBaseName = "Sample";
// mc++     }
// mc++     videoPath =
// mc++         new File(
// mc++             videoDirectory, videoBaseName + Long.toHexString(System.currentTimeMillis()) + ".mp4");
// mc++     File dir = videoPath.getParentFile();
// mc++     if (!dir.exists()) {
// mc++       dir.mkdirs();
// mc++     }
// mc++   }
// mc++ 
// mc++   private void stopRecordingVideo() {
// mc++     // UI
// mc++     recordingVideoFlag = false;
// mc++ 
// mc++     if (encoderSurface != null) {
// mc++       sceneView.stopMirroringToSurface(encoderSurface);
// mc++       encoderSurface = null;
// mc++     }
// mc++     // Stop recording
// mc++     mediaRecorder.stop();
// mc++     mediaRecorder.reset();
// mc++   }
// mc++ 
// mc++   private void setUpMediaRecorder() throws IOException {
// mc++ 
// mc++     mediaRecorder.setVideoSource(MediaRecorder.VideoSource.SURFACE);
// mc++     mediaRecorder.setOutputFormat(MediaRecorder.OutputFormat.MPEG_4);
// mc++ 
// mc++     mediaRecorder.setOutputFile(videoPath.getAbsolutePath());
// mc++     mediaRecorder.setVideoEncodingBitRate(bitRate);
// mc++     mediaRecorder.setVideoFrameRate(frameRate);
// mc++     mediaRecorder.setVideoSize(videoSize.getWidth(), videoSize.getHeight());
// mc++     mediaRecorder.setVideoEncoder(videoCodec);
// mc++ 
// mc++     mediaRecorder.prepare();
// mc++ 
// mc++     try {
// mc++       mediaRecorder.start();
// mc++     } catch (IllegalStateException e) {
// mc++       Log.e(TAG, "Exception starting capture: " + e.getMessage(), e);
// mc++     }
// mc++   }
// mc++ 
// mc++   public void setVideoSize(int width, int height) {
// mc++     videoSize = new Size(width, height);
// mc++   }
// mc++ 
// mc++   public void setVideoQuality(int quality, int orientation) {
// mc++     CamcorderProfile profile = CamcorderProfile.get(quality);
// mc++     if (profile == null) {
// mc++       profile = CamcorderProfile.get(CamcorderProfile.QUALITY_HIGH);
// mc++     }
// mc++     if (orientation == Configuration.ORIENTATION_LANDSCAPE) {
// mc++       setVideoSize(profile.videoFrameWidth, profile.videoFrameHeight);
// mc++     } else {
// mc++       setVideoSize(profile.videoFrameHeight, profile.videoFrameWidth);
// mc++     }
// mc++     setVideoCodec(profile.videoCodec);
// mc++     setBitRate(profile.videoBitRate);
// mc++     setFrameRate(profile.videoFrameRate);
// mc++   }
// mc++ 
// mc++   public void setVideoCodec(int videoCodec) {
// mc++     this.videoCodec = videoCodec;
// mc++   }
// mc++ }
