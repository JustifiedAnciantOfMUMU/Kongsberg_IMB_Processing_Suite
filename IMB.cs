using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;


namespace IMB_Data_Processing
{
    public class M3TcpTestClient
    {
        private FileStream m_fileStream;
        private byte[] m_buffer;
        private Bitmap m_bitMap;
        private int m_bitmapGain = 15;
        string IMBfilename;
        string outFilePath;
        public int BitmapGain
        {
            get
            {
                //lock (GainLock)
                {
                    return m_bitmapGain;
                }
            }
            set
            {
                //lock (GainLock)
                {
                    m_bitmapGain = value;
                }
            }
        }
        public Bitmap GetBitmap()
        {
            lock (m_bitMap)
            {
                return m_bitMap;
            }
        }

        public void StartIMBfileread(string inFilename, string outFilename)
        {
            IMBfilename=inFilename;
            outFilePath= outFilename;
            StartIMBfileread();
        }

        private void StartIMBfileread()
        {
            m_fileStream = null;
            try
            {
                m_fileStream = new FileStream(IMBfilename, FileMode.Open);
                m_bitMap = new Bitmap(1200, 600);
                IMBPacket packet = null;
                do
                {

                    packet = ParseIMBfile.ReadPacketFromStream(false, m_fileStream);

                    if (packet != null)
                    {
                        m_bitMap = IMBtoBitmap.IMBpacketToBitmap((IMBPacket)packet, m_bitMap.Width, m_bitMap.Height, BitmapGain);
                        string fName = System.IO.Path.Combine(outFilePath, CreateFilename(packet, Path.GetFileNameWithoutExtension(IMBfilename)));
                        m_bitMap.Save(fName, ImageFormat.Bmp);
                    }


                } while (packet != null);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Invalid File Path\n", "Error");
            }
            catch (IOException ioex)
            {
                Console.WriteLine("Lost connection to TCP server\n" + ioex.ToString(), "Error");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong with StartIMBfileread\n" + ex.ToString(), "Error");
            }
            finally
            {
                if (m_fileStream != null)
                    m_fileStream.Dispose();
                m_fileStream = null;
            }
        }

        private string CreateTimecode(IMBPacket packet)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Add the number of seconds to the epoch time
            DateTime timecode = epoch.AddSeconds(packet.dwTimeSec);
            timecode = timecode.AddMilliseconds(packet.dwTimeMillisec);

            String s_timecode = string.Format("{0:yyyyMMdd_HHmmss.fff}", timecode);

            return s_timecode;
        }

        private string CreateFilename(IMBPacket packet, String datasetname)
        {
            string filename = datasetname + "_" + packet.dwModeID + "_" + packet.fNearRange + "_" + packet.fFarRange + "_" + CreateTimecode(packet) + ".bmp";


            return filename;
        }





        public IMBPacket return_single_packet(string inFilename)
        {
            IMBfilename = inFilename;

            m_fileStream = new FileStream(IMBfilename, FileMode.Open);
            IMBPacket packet = null;
            packet = ParseIMBfile.ReadPacketFromStream(false, m_fileStream);

            return packet;
        }
    }


    public class IMBPacket
    {
        public const int HeaderSizeBytes = 56;
        public const int FooterSizeBytes = 44;

        public static byte[] SyncWord = { 0, 128, 0, 128, 0, 128, 0, 128 }; // Represents the sychronization sequence at the begining of a Packet Header

        public UInt32 dwVersion; 	// Version of this header
        public UInt32 dwSonarID; //Sonar identification
        public UInt32 DwSonarID //This is done in order to Bind the Data 
        {
            get { return dwSonarID; }
        }
        public UInt32[] dwSonarInfo = new UInt32[8];//Sonar information such as serial number, power up configurations
        public UInt32 dwTimeSec;  //Time stamp of current ping in seconds elapse since midnight (00:00:00), January 1, 1970
        public UInt32 DwTimeSec //This is done in order to Bind the Data 
        {
            get { return dwTimeSec; }
        }
        public UInt32 dwTimeMillisec; // Milliseconds part of current ping.
        public float fVelocitySound;    //Speed of sound in m/s
        public UInt32 nNumSamples;	//Total number of image samples.
        public float fNearRange;	//Minimum mode range in meters. No image below this range.
        public float fFarRange;    //Maximum mode range in meters.
        public float fSWST;        //Sampling Window Start Time in seconds
        public float fSWL;	        //Sampling Window Length
        public UInt16 nNumBeams;	//Total number of beams.
        public UInt16 wProcessingType;	            //	0: standard, 1: EIQ, 2: Profiling 3:Hybrid, 4:interferometry 5: Trawl 6: Profiling FFT
        public float[] BeamList = new float[1024]; // A list of angles for all beams up to the number of beams specified in nNumBeams field.
        public float fImageSampleInterval;	        //Image sample interval in seconds
        public UInt16 wImageDestination;           //Image designation, 0: main image window, n: zoom image window n, n<=4
        public UInt16 wReserved2;	    //Reserved field
        public UInt32 dwModeID;        //Unique mode ID, application ID
        public Int32 nNumHybridPRI; 	//Number of PRIs in a hybrid mode. 1 if not in hybrid mode.
        public Int32 nHybridIndex;		//0 based index used in hybrid mode up to (nNumHybridPRI -1)
        public UInt16 nPhaseSeqLength;	//Spatial phase sequence length
        public UInt16 iPhaseSeqIndex;	//Spatial phase sequence length index 0..(nPhaseSeqLength – 1)
        public UInt16 nNumImages;      //Number sub-images for one PRI
        public UInt16 iSubImageIndex;	//Sub-image index 0 .. (nNumImages – 1)
        public UInt32 dwSonarFreq; 	//Sonar frequency in Hz.
        public UInt32 dwPulseLength;	//Transmit pulse length in microseconds.
        public UInt32 dwPingNumber;	//Ping counter. Rolls back to zero if reaches 0xFFFFFFFF       
        public UInt32 DwPingNumber //This is done in order to Bind the Data 
        {
            get { return dwPingNumber; }
        }
        public float fRXFilterBW;	                    //RX filter bandwidth in Hz
        public float fRXNominalResolution;              //RX nominal resolution in metres
        public float fPulseRepFreq;	                    //Pulse Repetition Frequency in Hz
        public char[] strAppName = new char[128];	    //Application name, null terminated. Maximum 128 characters.
        public char[] strTXPulseName64 = new char[64];	//TX pulse name, null terminated. Maximum 64 characters.
        public TVG_PARAMS sTVGParameters = new TVG_PARAMS();	//TVG Parameters: (Available if dwVersion >=1) 12
        public float fCompassHeading;          //heading of current ping in decimal degrees
        public float MagneticVariation;	       //Magnetic variation in decimal degrees
        public float fPitch;        //Pitch in decimal degrees
        public float fRoll;	        //Roll in decimal degrees
        public float fDepth;	    //Depth in decimal meters
        public float Temperature;	//Temperature in decimal Celsius
        public float fXOffset;	    //Translational offset in X axis
        public float fYOffset;	    //Translational offset in Y axis
        public float fZOffset;	    //Translational offset in Z axis
        public float fXRotOffset;   //Rotational offset about X axis(Pitch offset).
        public float fYRotOffset;	//Rotational offset about Y axis(Roll offset)
        public float fZRotOffset;	//Rotational offset about Z axis(Yaw offset)
        public UInt32 dwMounting;	//Mounting orientation: (If dwVersion < 5  0: mounting Down or Aft 1: mounting Up or Fore) (If dwVersion >=5  0: Forward 1: Forward Inverted 2: Roll Right 3: Roll Left 4: Upward 5: Upward Inverted 6: Downward 7: Downward Inverted) (Deploy Config Head Mounting Parameter. For information only, mounting orientation is fully described by the fXRotOffset, fYRotOffset and fYRotOffset fields.)
        public double dbLatitude;	//Latitude of current ping in decimal degrees   
        public double dbLongitude;	//Longitude of current ping in decimal degrees
        public float fTXWST;	    //TX Window Start Time in seconds. Available if (dwVersion >= 4)
        public byte bHeadSensorVersion;	//Head Sensor Version
        public byte HeadHWStatus;	//Head hardware status: 0: Normal 1: High temperature warning 2: High temperature shutdown
        public byte byReserved1;	//Reserved field
        public byte byReserved2;	//Reserved field
        public float fInternalSensorHeading;	//Heading from internal sensor
        public float fInternalSensorPitch;	    //Pitch from internal sensor
        public float fInternalSensorRoll;	    //Roll from internal sensor
        public M3_ROTATOR_OFFSETS[] sAxesRotatorOffsets = new M3_ROTATOR_OFFSETS[3]//Rotator offset for a rotator mounted M3 system 48
            { new M3_ROTATOR_OFFSETS(), new M3_ROTATOR_OFFSETS(), new M3_ROTATOR_OFFSETS() };
        public UInt16 nStartElement;		    //Used if nNumImages > 1. Zero based start element of the sub array for the current sub image.
        public UInt16 nEndElement;	            //Used if nNumImages > 1. Zero based end element of the sub array for the current sub image.
        public char[] strCustomText1 = new char[32];	//Custom text field 1
        public char[] strCustomText2 = new char[32];	//Custom text field 2
        public float fLocalTimeOffset;         //Local time zone offset in decimal hours relative to UTC.
        public float fVesselSOG;	            //Vessel Speed Over Ground in knots
        public float fHeave;                   //Heave in meters.
        public float fPRIMinRange;	            //Minimum PRI range in meters.
        public float fPRIMaxRange;	            //Maximum PRI range in meters.
        public float fSoundSpeed_RT;	        //Real-time sound speed from SV sensors.
        public byte[] Reserved = new byte[3856];
        public float[,] fPacketBody;
    }
    public class M3_ROTATOR_OFFSETS
    {
        public float fOffsetA; //Offset A in meters
        public float fOffsetB; //Offset B in meters
        public float fOffsetR; //Offset R in meters
        public float fRotAngle; //Rotator Angle in degrees
    }
    public class TVG_PARAMS
    {
        public UInt16 SpreadingCoeff; //Factor A, the spreading coefficient
        public UInt16 AbsorptionCoeff; //FactorB, the absorption coefficient in dB/km
        public float fTVGCruve; //Factor C, the TVG curve offset in dB
        public float fMaxGain; //Factor L, the maximum gain limit in dB
    }



    public class ParseIMBfile
    {
        private ObservableCollection<object> currentPacketList;
        private IMBPacket currentPacket = null;
        public static int indexOf(byte[] data, byte[] pattern)
        {
            int[] failure = computeFailure(pattern);

            int j = 0;
            if (data.Length == 0)
                return -1;
            for (int i = 0; i < data.Length; i++)
            {
                while (j > 0 && pattern[j] != data[i])
                {
                    j = failure[j - 1];
                }
                if (pattern[j] == data[i])
                    j++;
                if (j == pattern.Length)
                {
                    return i - pattern.Length + 1;
                }
            }
            return -1;
        }
        private static int[] computeFailure(byte[] pattern)
        {
            int[] failure = new int[pattern.Length];

            int j = 0;
            for (int i = 1; i < pattern.Length; i++)
            {
                while (j > 0 && pattern[j] != pattern[i])
                {
                    j = failure[j - 1];
                }
                if (pattern[j] == pattern[i])
                {
                    j++;
                }
                failure[i] = j;
            }
            return failure;
        }

        public void Parsebuffer(byte[] buffer)
        {
            currentPacketList = ParseByteArraytoPacketList(buffer);
            if (currentPacketList.Count == 0)
                currentPacket = null;
            else
                currentPacket = (IMBPacket)currentPacketList.ElementAt(0);
        }

        public IMBPacket CurrentPacket
        {
            get { return currentPacket; }
        }

        // Modified function from Sonar Record Viewer Code
        // Parses for the first complete IMB packet and returns it 
        public ObservableCollection<object> ParseByteArraytoPacketList(byte[] inbuffer)
        {
            ObservableCollection<object> PacketContainer0 = new ObservableCollection<object>(); //Sequenced
            //int timestried = 0;
            object PacketObject;

            bool fileIsSMB = false;
            try
            {
                MemoryStream fileStream = new MemoryStream(inbuffer);
                using (fileStream)
                {
                    while (fileStream.Length >= fileStream.Position)
                    {

                        PacketObject = ReadPacketFromStream(fileIsSMB, fileStream);
                        if (PacketObject != null)
                            PacketContainer0.Add(PacketObject);
                        else
                            break;
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong while parsing\n" + ex.ToString(), "Error");
            }
            return PacketContainer0;
        }

        public static IMBPacket ReadPacketFromStream(bool fileIsSMB, Stream fileStream)
        {
            UInt16 datatype = 0;
            int syncWordOffset = 0;
            int packetSizeHeader;
            int packetSizeFooter;

            int packetSize = IMBPacket.HeaderSizeBytes + 100;
            byte[] buffer = new byte[packetSize];

            int bytesRead = fileStream.Read(buffer, 0, packetSize); //creates the temporary buffer so that we can obtain the actual size of the next packet. Note: The position of fileStream would have changed                     
            if (bytesRead <= IMBPacket.HeaderSizeBytes && fileIsSMB == false)
                return null;

            if (fileIsSMB == false) //non-smb (imb mmb tmb)
            {
                //Represents the Index of the beginning of the found Sync Word
                syncWordOffset = indexOf(buffer, IMBPacket.SyncWord);
                if (syncWordOffset == -1) //means no instance of the SyncWord has been found. If none is found the Search word for smb is searched for.
                {
                    return null;
                }
                else // non-smb (imb mmb tmb) parsing
                {
                    fileStream.Position += syncWordOffset - bytesRead; // Place the position of the fileStream at the start of the SyncWord
                    if ((syncWordOffset + 52) >= buffer.Length)
                    {
                        syncWordOffset = 0;
                        bytesRead = fileStream.Read(buffer, 0, packetSize);
                        fileStream.Position += syncWordOffset - bytesRead;
                    }
                    packetSize = (int)BitConverter.ToUInt32(buffer, syncWordOffset + 52) + syncWordOffset + IMBPacket.HeaderSizeBytes + IMBPacket.FooterSizeBytes; // Packet size = Packet Body + the bytes before SyncWord + Header Size + Footer Size
                    buffer = new byte[packetSize];
                    bytesRead = fileStream.Read(buffer, 0, packetSize);

                    if (fileStream.Length < fileStream.Position)
                        return null;

                    //Packet Sizes obtained from the Packet Header & Footer. To be checked for uniformity 
                    packetSizeHeader = (int)BitConverter.ToUInt32(buffer, syncWordOffset + 52);
                    if (packetSizeHeader + syncWordOffset + 56 > buffer.Length || packetSizeHeader < 0)
                        return null;
                    packetSizeFooter = (int)BitConverter.ToUInt32(buffer, (int)packetSizeHeader + syncWordOffset + 56);
                    if (packetSizeHeader != packetSizeFooter) //Compare if both values are the same, as outlined in the "Data integrity checking procedure"
                        return null;

                    // Increment the index to be set right before the DataType
                    datatype = BitConverter.ToUInt16(buffer, syncWordOffset + 8);
                    syncWordOffset += 56;  //Increment the index to be set right before the Data header in the Hex code
                }
            }

            switch (datatype)
            {
                case 4098: //IMB
                    return ParseByteArraytoIMBPacket(buffer, syncWordOffset);
                default: //Another type
                    return null;
            }
        }

        private static IMBPacket ParseByteArraytoIMBPacket(byte[] buffer, int syncWordOffset)
        {
            IMBPacket PacketObject = new IMBPacket();
            byte[] bytesSegment = new byte[8512];
            Array.Copy(buffer, syncWordOffset, bytesSegment, 0, 8512); //Copy only the needed part of the array to be passed on to be parsed
            object objPacket = PacketObject;
            ReadClass(ref objPacket, ref bytesSegment); //Parse all data into the newly initialized IMBPacket

            PacketObject.fPacketBody = new float[PacketObject.nNumBeams, PacketObject.nNumSamples * 2];
            Buffer.BlockCopy(buffer, syncWordOffset + 8512, PacketObject.fPacketBody, 0, (PacketObject.fPacketBody.Length * 4));//Directly copy the byte array, starting from the byte after PacketHeader(Index) + Dataheader (including the reserved bytes), to the memory location of the PacketBody Array. Length to copy is # of floats * 4                     

            return PacketObject;
        }
        /// <summary>
        /// Read in an object that has been previously determined to be a Class. It iterates through each field and fill it with
        /// the appropriate data from the read in byte array
        /// </summary>
        /// <param name="obj">Object that was determined to be a class and whose fields are to be populated</param>
        /// <param name="bytesIn">Byte Array form which data needs to be parsed from</param>
        public static void ReadClass(ref object obj, ref byte[] bytesIn)
        {
            Type type = obj.GetType();
            FieldInfo[] fiArr = type.GetFields(BindingFlags.Public | BindingFlags.Instance); //Create an array containing all the fields of the class
            for (int i = 0; i < fiArr.Length; i++) //Looping through each field, call the ReadClass recursively to parse the byte array and populate the field.
            {
                object objF = fiArr[i].GetValue(obj);
                ReadItem(ref objF, ref bytesIn);
                fiArr[i].SetValue(obj, objF);
            }
        }

        /// <summary>
        /// Read in an object and determine whether it is a class, array or a primitive, and call the appropriate function to populate it.
        /// </summary>
        /// <param name="obj">Object to populate</param>
        /// <param name="bytesIn">Byte Array from which data will be parsed from</param>
        private static void ReadItem(ref object obj, ref byte[] bytesIn)
        {
            if (obj != null)
            {
                //Based on what category the object passed on belongs to (Class, Array, Primitive), call the appropriate function
                if (obj.GetType().IsArray)
                {
                    ReadArray(ref obj, ref bytesIn);
                }
                else if (obj.GetType().IsClass)
                {
                    ReadClass(ref obj, ref bytesIn);
                }
                else
                {
                    TryReadPrimitive(ref obj, ref bytesIn);
                }
            }
        }

        /// <summary>
        /// Takes in an object that has been determiend to be an Array.It iterate through each field and call ReadItem to classify and fill the element with
        /// the approriate data from the read in byte array
        /// </summary>
        /// <param name="obj">Object that was determined to be an Array and whose elements are to be field</param>
        /// <param name="bytesIn">Byte Array form which data needs to be parsed from</param>
        private static void ReadArray(ref object obj, ref byte[] bytesIn)
        {

            Array array = (Array)obj;//Create an Array to hold the obtained data
            if (array.GetType().GetElementType().IsPrimitive == true && array.GetType().GetElementType() != typeof(char)) //Checks if the array is made of primitive types, as to check if block copy is possible
            {
                Buffer.BlockCopy(bytesIn, 0, array, 0, Buffer.ByteLength(array)); //Instead of going element by element, copy entire sections of the byte data into the array
                byte[] temp = new byte[bytesIn.Length - Buffer.ByteLength(array)];
                Array.Copy(bytesIn, Buffer.ByteLength(array), temp, 0, temp.Length); //Delete the part of the byte data that has been copied 
                bytesIn = temp;
            }
            else if (array.Rank == 1)
            {
                for (int j = 0; j < array.Length; j++)//For as much elements there are in the array, call the ReadClass recursively to populate the created holder array with obtained data
                {
                    object objF = array.GetValue(j);
                    ReadItem(ref objF, ref bytesIn);
                    array.SetValue(objF, j);
                }
            }
            else if (array.Rank == 2)//For as much elements there are in the array,  call the ReadClass recursively to populate the created holder array with obtained data
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    for (int k = 0; k < array.GetLength(1); k++)
                    {
                        object objF = array.GetValue(j, k);
                        ReadItem(ref objF, ref bytesIn);
                        array.SetValue(objF, j, k);
                    }
                }
            }
            else if (array.Rank == 3)//For as much elements there are in the array,  call the ReadClass recursively to populate the created holder array with obtained data
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        for (int k = 0; k < array.GetLength(2); k++)
                        {
                            object objF = array.GetValue(i, j, k);
                            ReadItem(ref objF, ref bytesIn);
                            array.SetValue(objF, i, j, k);
                        }
                    }
                }
            }
            obj = array;//Copy the array obtained from the byte data into the field that needs to be populated
        }

        /// <summary>
        /// Read in an object and a byte array and set the object to reflect the value converted from the read in byte array
        /// </summary>
        /// <param name="obj">An object which represents one of the fields to be read</param>
        /// <param name="bytesIn">The Byte Array from which the value of the field will be obtained</param>
        /// <returns>True if successful, False otherwise</returns>
        private static void TryReadPrimitive(ref object obj, ref byte[] bytesIn)
        {
            Type type = obj.GetType();
            int dataSize = 0;
            try
            {
                if (type.IsEnum)
                {
                    obj = BitConverter.ToInt32(bytesIn, 0);
                    dataSize = sizeof(Int32);
                }
                else
                {
                    TypeCode tc;
                    tc = Type.GetTypeCode(type); //Depending on the the of the field from our MyTestData we will translate the Hex code and take note of the length of the hex data parsed
                    switch (tc)
                    {
                        case TypeCode.Boolean:
                            dataSize = sizeof(bool);
                            obj = BitConverter.ToBoolean(bytesIn, 0);
                            break;
                        case TypeCode.Byte:
                            dataSize = sizeof(Byte);
                            obj = bytesIn[0];
                            break;
                        case TypeCode.SByte:
                            dataSize = sizeof(SByte);
                            obj = bytesIn[0];
                            break;
                        case TypeCode.Int16:
                            dataSize = sizeof(Int16);
                            obj = BitConverter.ToInt16(bytesIn, 0);
                            break;
                        case TypeCode.UInt16:
                            dataSize = sizeof(UInt16);
                            obj = BitConverter.ToUInt16(bytesIn, 0);
                            break;
                        case TypeCode.Char:
                            UTF8Encoding temp = new UTF8Encoding();
                            dataSize = 1;
                            obj = temp.GetChars(bytesIn, 0, 1);
                            Array array = (Array)obj;
                            obj = array.GetValue(0);
                            break;
                        case TypeCode.Int32:
                            dataSize = sizeof(Int32);
                            obj = BitConverter.ToInt32(bytesIn, 0);
                            break;
                        case TypeCode.UInt32:
                            dataSize = sizeof(UInt32);
                            obj = BitConverter.ToUInt32(bytesIn, 0);
                            break;
                        case TypeCode.Int64:
                            dataSize = sizeof(Int64);
                            obj = BitConverter.ToInt64(bytesIn, 0);
                            break;
                        case TypeCode.UInt64:
                            dataSize = sizeof(UInt64);
                            obj = BitConverter.ToUInt64(bytesIn, 0);
                            break;
                        case TypeCode.Single:
                            dataSize = sizeof(Single);
                            obj = BitConverter.ToSingle(bytesIn, 0);
                            break;
                        case TypeCode.Double:
                            dataSize = sizeof(Double);
                            obj = BitConverter.ToDouble(bytesIn, 0);
                            break;
                        case TypeCode.DateTime:
                            dataSize = sizeof(Int64);
                            long prevresult = BitConverter.ToInt64(bytesIn, 0);
                            obj = new DateTime(prevresult);
                            break;
                        default:
                            throw new Exception("Unknown type: " + type.ToString());
                    }
                }
                byte[] byteSegment = new byte[bytesIn.Length - dataSize]; //Create a buffer array to hold the new array which is the old byte array with the part that has been parsed taken away
                Array.Copy(bytesIn, dataSize, byteSegment, 0, byteSegment.Length); //Copy the unread parts of the old byte array into the buffer
                bytesIn = byteSegment; //Assign the buffer as the new byte array
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine("TryReadPrimitive(): " + ex.Message);
            }
        }

    }

}

